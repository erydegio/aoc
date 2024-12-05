defmodule Day5 do

  def run(path) do
    {orders, data} = parse(path)
    Enum.map(data, fn l -> middle_from_valid_sequence(l, orders) end)
    |> Enum.sum
  end

  def run_part2(path) do
    {orders, data} = parse(path)
    Enum.map(data, fn l -> middle_from_not_valid_sequence(l, orders) end)
    |> Enum.sum
   end

  defp middle_from_valid_sequence([curr | rest] = numbers, orders) do
    if is_valid_sequence?(rest, curr, orders), do: Enum.at(numbers, div(length(numbers), 2)), else: 0
  end

  defp middle_from_not_valid_sequence([curr | rest] = numbers, orders) do
    if not is_valid_sequence?(rest, curr, orders),
     do: sort_list(numbers, orders) |> Enum.at(div(length(numbers), 2)),
     else: 0
  end

  defp is_valid_sequence?([], _curr, orders), do: true
  defp is_valid_sequence?([next | rest], current, orders) do
    case Map.get(orders, current) do
      nil -> false
      values -> if next in values, do: is_valid_sequence?(rest,next, orders), else: false
    end
  end

  def sort_lists(lists, orders) do
    Enum.map(lists, fn list -> sort_list(list, orders) end)
  end

  defp sort_list(list, orders) do
    Enum.sort(list, fn a, b ->
      case {Map.get(orders, a), Map.get(orders, b)} do
        {nil, _} -> false
        {_, nil} -> true
        {order_a, order_b} -> Enum.find_index(order_a, &(&1 == b)) < Enum.find_index(order_b, &(&1 == a))
      end
    end)
  end

  defp parse(path) do
    [part1, part2] = File.read!(path) |> String.split("\n\n") |> Enum.map(&String.split(&1, "\n"))

    orders =
      part1
      |> Enum.reduce(%{}, fn line, acc -> [key, value] = String.split(line, "|")
      |> Enum.map(&String.to_integer/1)

    Map.update(acc, key, [value], &(&1 ++ [value])) end)
    
    data =
      part2
      |> Enum.map(&String.split(&1, ","))
      |> Enum.map(fn  en -> Enum.map(en, fn n -> String.to_integer(n)end)end)

    {orders, data}
  end
end
