ExUnit.start()
Code.require_file(Path.join(__DIR__, "../5/day5.exs"))

defmodule Day5Tests do
use ExUnit.Case

  test "part 1 - example" do
    assert Day5.run("example.txt") == 143
  end

  test "part 1 " do
    assert Day5.run("input.txt") == 4924
  end

  test "part 2 - example" do
    assert Day5.run_part2("example.txt") == 123
  end

  test "part 2 " do
    assert Day5.run_part2("input.txt") == 6085
  end
end
