defmodule Day3 do

  def run(path),
   do: parse(path, ~r/mul\(\d+,\d+\)/) |> Enum.reduce(0, &multiply/2)

  def run_part2(path) do
    parse(path, ~r/(mul\(\d+,\d+\)|don't\(\)|do\(\))/)
    |> Enum.reduce({0, true}, fn
      "don't()", {acc, _} -> {acc, false}
      "do()", {acc, _}    -> {acc, true}
      elem, {acc, true}   -> {multiply(elem, acc), true}
      _, {acc, false}     -> {acc, false}
    end)
    |> elem(0)
  end

  defp multiply(elem, acc) do
    [_, n1, n2] = Regex.run(~r/mul\((\d+),(\d+)\)/, elem)
    acc + String.to_integer(n1) * String.to_integer(n2)
  end

  defp parse(path, pattern) do
    {:ok, data} = File.read(path)
    Regex.scan(pattern, data)
    |> Enum.map(&Enum.uniq/1)
    |> List.flatten
  end
end
