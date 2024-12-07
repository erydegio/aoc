ExUnit.start()
Code.require_file(Path.join(__DIR__, "../7/day7.exs"))

defmodule Day6Tests do
use ExUnit.Case

  test "part 1 - example" do
    assert Day7.run("example.txt") == 3749
  end

  test "part 1 " do
    assert Day7.run("input.txt") == 3245122495150
  end

  test "part 2 - example" do
    assert Day7.run_part2("example.txt") == 11387
  end

  test "part 2 " do
    assert Day7.run_part2("input.txt") == 105517128211543
  end
end
