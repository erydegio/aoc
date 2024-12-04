#in todo

defmodule TobogganTrajectory do
#{3,1}, {1,1}, {5,1}, {7,1},
  def run(path), do: read_and_map(path, [{3,1}])
  def run2(path), do: read_and_map(path, [{1, 2}])

  defp read_and_map(path, slopes) do
    path
    |> File.stream!
    |> Stream.map(&String.trim/1)
    |> Stream.reject(&(&1 == ""))
    |> how_many_trees2(slopes)
  end

  defp how_many_trees2(lines, slopes) do
    slopes
    |> Enum.map(fn {right, down} -> how_many_trees(lines, right, down) end)
    #|> Enum.reduce(1, &*/2)
  end

  defp how_many_trees(lines, right, down) do

    lines
    |> Enum.with_index()
    |> Enum.filter(fn {_line, index} -> rem(index, down) == 0 end)
   # |> Enum.map(fn {line, index} -> String.at(line, rem(index * right, String.length(line))) end)
   # |> Enum.count(&(&1 == "#"))
  end
end
