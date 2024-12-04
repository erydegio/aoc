defmodule HistorianHysteria  do

  def run(path), do: read_and_map(path) |> find_distance
  def run_part2(path) , do: read_and_map(path) |> find_similarity_score

  defp find_similarity_score({list1, list2}) do
    for n1 <- list1, reduce: 0 do
      acc -> Enum.count(list2, &(&1 == n1)) * n1 + acc
    end
  end

  defp find_distance({list1, list2}),
    do: Enum.zip_reduce(Enum.sort(list1), Enum.sort(list2), 0, fn n1, n2, acc -> acc + abs(n1 - n2) end)


  defp read_and_map(path) do
    File.stream!(path)
    |> Stream.map(&String.trim/1)
    |> Stream.map(&String.split(&1, " ", trim: true))
    |> Enum.reduce({[], []},
     fn [a, b], {list1, list2} -> {list1 ++ [String.to_integer(a)], list2 ++ [String.to_integer(b)]} end)
  end
end
