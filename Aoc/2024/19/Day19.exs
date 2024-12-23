defmodule Day19 do

  def part_1(path) do
    {towel_patterns, designs} = parse(path)

    Enum.reduce(designs, 0,fn design, acc ->
      if design_possible?(design, towel_patterns), do: acc + 1, else: acc end)
  end

  def part_2(path) do
    {towel_patterns, designs} = parse(path)

    Enum.reduce(designs, 0, fn design, acc ->
      acc + count_ways(design, towel_patterns) end)
  end

  defp parse(path) do
    [towel_patterns_str, input_str] =
      path
      |> File.read!()
      |> String.split("\n\n", trim: true)

    towel_patterns = String.split(towel_patterns_str, ", ", trim: true)
    designs = String.split(input_str, "\n", trim: true)

    {towel_patterns, designs}
  end

  defp design_possible?(towel, patterns) do
    n = String.length(towel)

    init(n)
    |> update(towel, patterns, n)
    |> Enum.at(n)
  end

  defp init(n) do
    List.duplicate(false, n + 1)
    |> List.replace_at(0, true)
  end

  defp update(dp, towel, patterns, n) do
    1..n
    |> Enum.reduce(dp, fn i, dp ->
      patterns
      |> Enum.reduce(dp, fn pattern, dp -> update_pattern(dp, towel, pattern, i) end)
    end)
  end

  defp update_pattern(dp, towel, pattern, i) do
    len = String.length(pattern)
    if i >= len and String.slice(towel, i - len, len) == pattern and Enum.at(dp, i - len) do
      List.replace_at(dp, i, true)
    else
      dp
    end
  end

  defp count_ways(towel, patterns) do
    n = String.length(towel)
    dp = List.duplicate(0, n + 1) |> List.replace_at( 0, 1)

    dp =
      1..n
      |> Enum.reduce(dp, fn i, dp ->
        patterns
        |> Enum.reduce(dp, fn pattern, dp ->
          update_ways(dp, towel, pattern, i)
        end)
      end)

    Enum.at(dp, n)
  end

  defp update_ways(dp, towel, pattern, i) do
    len = String.length(pattern)
    if i >= len and String.slice(towel, i - len, len) == pattern do
      List.update_at(dp, i, &(&1 + Enum.at(dp, i - len)))
    else
      dp
    end
  end
end
