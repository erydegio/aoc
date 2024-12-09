defmodule Day9 do

  def run(path) do
    {:ok, content} = File.read(path)
    content
    |> create_data
    |> Enum.with_index
    |> Enum.reduce(0, fn item, acc -> compact(item,acc) end)
  end

  def compact({:space, _}, acc), do: acc
  def compact({id, index}, acc) do
    IO.puts("#{id}checking...")
    acc + (id * index)
   end

  def run_part2(path) do

  end

  def is_space?(0), do: false
  def is_space?(1), do: true
  def is_space?(index) when rem(index, 2) == 0, do: false
  def is_space?(index), do: true

  def move_memory(list) do

    if is_continuous?(list) do
      list
    else
      first_space =  Enum.find_index(list, &(&1 == :space))
      last_file = Enum.reduce_while(
                    Enum.with_index(list), -1, fn {x, index}, acc -> if x != :space, do: {:cont,index}, else: {:cont,acc}  end)

      swap(list, first_space, last_file) |> move_memory
    end
  end

  defp create_data(string) do
      string
      |> String.graphemes()
      |> Enum.with_index()
      |> compose_list( 0)
      |> List.flatten
      |> move_memory
  end

  defp compose_list([] , id), do: []
  defp compose_list([{char, index} | rest] , id) do
    times = String.to_integer(char)
    if is_space?(index),
      do: [List.duplicate(:space, times) | compose_list(rest, id)],
      else: [List.duplicate(id, times) | compose_list(rest, id+1)]
  end

   def swap(list, index1, index2) do
    {elem1, elem2} = {Enum.at(list, index1), Enum.at(list, index2)}

    list
    |> List.replace_at(index1, elem2)
    |> List.replace_at(index2, elem1)
  end

  def is_continuous?(list) do
    first_space_index = Enum.find_index(list, fn x -> x == :space end)
    check_spaces(list, first_space_index)
  end

  defp check_spaces(list, index) when index == length(list), do: true
  defp check_spaces(list, index) do
    if Enum.at(list, index) == :space, do: check_spaces(list, index + 1), else: false
  end
end

# Each file on disk also has an ID number
#  a disk map like 12345 would represent a one-block file, two blocks of free space, a three-block file
#the index represents an id number 1 (id0), 3(id1) ecc

#MOVE MEMORY FROM FILE TO DISK SPACE
#scorro tutte le mappe con :file partendo da quello con id + grande
# scorro tutte le mappe con :space partendo da quelli con id più piccolo
#
