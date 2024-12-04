defmodule RedNosedReports  do

  def run(path), do: read_and_map(path) |> find_safe

  def run_part2(path),
    do: read_and_map(path) |> Enum.count(&find_safe_part2/1)

  defp find_safe(list), do: Enum.count(list, &is_safe?(&1, true, true))

  defp find_safe_part2(list),
   do: is_safe?(list, true, true) or is_safe_part2(list)

  defp is_safe_part2(list) do
    list
    |> Enum.with_index()
    |> Enum.any?(fn {_, index} -> is_safe?(List.delete_at(list, index), true, true) end)
  end

  def is_safe?(_ , false, false), do: false
  def is_safe?([n1, n2 | rest], _, _) when abs(n1 - n2) < 1 or abs(n1 - n2) > 3, do: false
  def is_safe?([n], _, _), do: true
  def is_safe?([n1, n2 | rest], decreasing, increasing) do
    cond do
      n1 < n2 -> is_safe?([n2 | rest], false, increasing)
      n1 > n2 -> is_safe?([n2 | rest], decreasing, false)
    end
  end

  defp read_and_map(path) do
    File.stream!(path)
    |> Stream.map(&String.trim/1)
    |> Stream.map(&String.split(&1, " ", trim: true))
    |> Stream.map(fn sublist -> Enum.map(sublist, &String.to_integer/1) end)
    |> Enum.to_list()
  end
end
