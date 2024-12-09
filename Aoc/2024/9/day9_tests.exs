ExUnit.start()
Code.require_file(Path.join(__DIR__, "../9/day9.exs"))

defmodule Day9Tests do
use ExUnit.Case

  test "part 1 - example" do
    assert Day9.run("example.txt") == 1928
  end

  @tag timeout: :infinity
  test "part 1 " do
    assert Day9.run("input.txt") == 3245122495150
  end

  # test "part 2 - example" do
  #   assert Day9.run_part2("example.txt") == 11387
  # end

  # test "part 2 " do
  #   assert Day9.run_part2("input.txt") == 105517128211543
  # end
end

#1234
# type: file, id:0, blocks:1, type: :space, id:1, blocks2,
