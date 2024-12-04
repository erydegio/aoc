defmodule PasswordPhilosophy do

  def run(path), do: read_and_map(path, &part1?/1)
  def run2(path), do: read_and_map(path, &part2?/1)

  defp how_many_valid(psw_list, func),
   do: Enum.reduce(psw_list, 0, fn elem, acc -> if func.(elem), do: acc + 1, else: acc end)

  defp part1?(string) do
    [_, from, to, letter, psw] = Regex.run(~r/(\d+)-(\d+) (\w): (\w+)/, string)
     ~r/^(?:[^#{letter}]*#{letter}){#{from},#{to}}[^#{letter}]*$/ |> Regex.match?(psw)
  end

  defp part2?(string) do
    case match(string) do
      {true, true} -> false
      {true, false} -> true
      {false, true} -> true
      _ -> false
    end
  end

  defp match(string) do
    [_, p1, p2, letter, psw] = Regex.run(~r/(\d+)-(\d+) (\w): (\w+)/, string)
    {
      ~r/^.{#{String.to_integer(p1) - 1}}#{letter}/ |> Regex.match?(psw),
      ~r/^.{#{String.to_integer(p2) - 1}}#{letter}/ |> Regex.match?(psw)
    }
  end

  defp read_and_map(path, func) do
    path
    |> File.stream!
    |> Stream.map(&String.trim/1)
    |> Stream.reject(&(&1 == ""))
    |> how_many_valid(func)
  end
end
