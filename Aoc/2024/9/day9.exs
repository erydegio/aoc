defmodule Day9 do

  def run(path) do
    {:ok, content} = File.read(path)
    content
    |> create_data
    |> move_memory
    |> count_from_index
  end

  def run_part2(path) do
    {:ok, content} = File.read(path)
    content
    |> create_data_grouped
    |> move_memory_segment
    |> Enum.map(fn {list, _index} -> list end)
    |> List.flatten
    |> count_from_index
  end

  defp count_from_index(list), do:
     Enum.with_index(list) |> Enum.reduce(0, fn item, acc -> count(item,acc) end)

  def count({:space, _}, acc), do: acc
  def count({id, index}, acc), do: acc + (id * index)

  defp is_space?(0), do: false
  defp is_space?(1), do: true
  defp is_space?(index) when rem(index, 2) == 0, do: false
  defp is_space?(_index), do: true

  defp move_memory(list) do
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
    |> compose_list(0)
    |> List.flatten
  end

  defp create_data_grouped(string) do
    string
    |> String.graphemes()
    |> Enum.with_index()
    |> compose_list(0)
    |> Enum.filter(&(&1 != []))
  end

  defp move_memory_segment(list) do
    origial_with_index = list |> Enum.with_index
    list
    |> Enum.with_index
    |> Enum.filter(fn {sublist, _index} -> Enum.any?(sublist, &is_integer/1)end)
    |> Enum.reverse
    |> move_memory_segment(origial_with_index)
  end

  defp move_memory_segment([], res_list), do: res_list
  defp move_memory_segment([{data_from, index_from} = file_data_from | rest], res_list) do

      case Enum.find(res_list, :not_found, fn {list_to, index_to} ->
        Enum.count(list_to, fn e -> e == :space end) >= length(data_from) and index_from > index_to  end) do

        :not_found -> move_memory_segment(rest, res_list)
        res_list_to -> move_memory_segment(rest, swap_segments(res_list, file_data_from, res_list_to ))
      end
  end

  defp swap_segments(souce_list, {file_data_from, index_from}, {found_item_to, index_to}) do
    length_to = length(found_item_to)

    {list_to, list_from} =  move_memory(found_item_to ++ file_data_from) |> Enum.split(length_to)

    souce_list
    |> List.replace_at(index_to, {list_to, index_to})
    |> List.replace_at(index_from, {list_from, index_from})
  end

  defp compose_list([] , _id), do: []
  defp compose_list([{char, index} | rest] , id) do
    times = String.to_integer(char)
    
    if is_space?(index),
      do: [List.duplicate(:space, times) | compose_list(rest, id)],
      else: [List.duplicate(id, times) | compose_list(rest, id+1)]
  end

   defp swap(list, index1, index2) do
    {elem1, elem2} = {Enum.at(list, index1), Enum.at(list, index2)}

    list
    |> List.replace_at(index1, elem2)
    |> List.replace_at(index2, elem1)
  end

  defp is_continuous?(list) do
    first_space_index = Enum.find_index(list, fn x -> x == :space end)
    check_spaces(list, first_space_index)
  end

  defp check_spaces(list, index) when index == length(list), do: true
  defp check_spaces(list, index) do
    if Enum.at(list, index) == :space, do: check_spaces(list, index + 1), else: false
  end
end
