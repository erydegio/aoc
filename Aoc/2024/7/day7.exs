defmodule Day7 do

  def run(path) do
    operators = [:*, :+]

    parse_file(path)
    |> Enum.filter(&(is_valid?(&1, operators)))
    |> Enum.reduce( 0, fn [h| _], acc -> acc + h end)
  end

  def run_part2(path) do
    operators = [:*, :+, :||]

    parse_file(path)
    |> Enum.filter(&(is_valid?(&1, operators)))
    |> Enum.reduce( 0, fn [h| _], acc -> acc + h end) 
  end

  defp is_valid?([test_value | rest] = sequence, operators) do
    results = generate_combinations(rest, operators) |> apply_combinations(rest)
    test_value in results
  end

  def generate_combinations(sequence, operators) do
    slots = length(sequence) - 1
    generate_combinations(slots, operators, [], [])
  end

  defp generate_combinations(0, _, acc, result), do: [Enum.reverse(acc) | result]
  defp generate_combinations(slots, operators, acc, result) do
    Enum.reduce(operators, result, fn op, res -> new_acc = [op | acc]
    generate_combinations(slots - 1, operators, new_acc, res) end)
  end

  defp apply_combinations(combinations, sequence) do
    Enum.map(combinations, fn combination -> apply_combination(combination, sequence) end)
  end
  defp apply_combination(ops, sequence) do
    Enum.reduce(Enum.zip(ops, tl(sequence)), hd(sequence), fn {op, num}, acc -> apply_op(op, acc, num) end)
  end

  defp apply_op(:+, a, b), do: a + b
  defp apply_op(:*, a, b), do: a * b
  defp apply_op(:||, a, b), do: a * :math.pow(10, Integer.digits(b) |> length) + b |> trunc

  def parse_file(path) do
    File.read!(path)
    |> String.trim()
    |> String.split("\n")
    |> Enum.map(&parse_line/1)
  end

  defp parse_line(line) do
    [first | rest] = String.split(String.replace(line, ":", ""), " ")
    [String.to_integer(first)] ++ Enum.map(rest, &String.to_integer/1)
  end
end
