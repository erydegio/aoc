defmodule ReportRepair do

  #open the file
  def run(path) do
    read_and_map(path) |> loop(&find_double_sum/2)
  end

  def run_part2(path) do

    [[ result| _] | _] =
    read_and_map(path) |> find_triplet_sum

    result
  end

  defp loop([head | rest], find) do
     case find.(rest, head) do
      :not_found -> loop(rest, find)
      {first, second} -> first * second
    end
  end
  defp loop([], _), do: :not_found

  defp find_double_sum([head | _], acc) when acc + head == 2020, do: {acc, head}
  defp find_double_sum([_ | tail], acc), do: find_double_sum(tail, acc)
  defp find_double_sum([], _), do: :not_found

  defp find_triplet_sum(numbers) do
    for a <- numbers, b <- numbers, c <- numbers, a != b and b != c and a != c and a + b + c == 2020, do: [a*b*c]
  end

  defp read_and_map(path) do
    path
    |> File.stream!
    |> Stream.map(&String.trim/1)
    |> Enum.map(&String.to_integer/1)
  end
end
