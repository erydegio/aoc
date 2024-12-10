defmodule Day10 do
  def run_part2(path) do
    map = parse_map(path)
    start_points = find_start(map)
    Enum.reduce(start_points, %{}, fn {x, y}, acc ->
      score = explore(map, x, y, 0, MapSet.new())
      Map.put(acc, {x, y}, score)
    end)
    |> Enum.map(fn {_key, value} -> value end)
    |> Enum.sum()
  end

  defp find_start(map) do
    for x <- 0..(length(map) - 1),
        y <- 0..(length(List.first(map)) - 1),
        Enum.at(map, x) |> Enum.at(y) == 0,
        do: {x, y}
  end

  defp explore(map, x, y, 9, visited), do: 1
  defp explore(map, x, y, actual_pos, visited) do

      directions = [{0, 1}, {1, 0}, {0, -1}, {-1, 0}]

      Enum.reduce(directions, 0, fn {dx, dy}, acc ->
        next_x = x + dx
        next_y = y + dy
        next_coords = {next_x, next_y}

        if valid_position?(map, next_x, next_y) and correct_high?(map, actual_pos, next_x, next_y) and not MapSet.member?(visited, next_coords) do
          new_visited = MapSet.put(visited, next_coords)
          acc + explore(map, next_x, next_y, actual_pos + 1, new_visited)
        else
          acc
        end
      end)
  end

  defp correct_high?(map, prev_p, next_x, next_y) do
    next = Enum.at(map, next_x) |> Enum.at(next_y)
    next == prev_p + 1
  end

  defp valid_position?(grid, x, y) do
    x >= 0 and x < length(grid) and y >= 0 and y < length(List.first(grid))
  end

  def parse_map(path) do
    {:ok, content} = File.read(path)
    content
    |> String.split("\n", trim: true)
    |> Enum.map(fn row ->
      row
      |> String.graphemes()
      |> Enum.map(&String.to_integer/1)
    end)
  end
end
