ExUnit.start()
Code.require_file(Path.join(__DIR__, "../3/day3.exs"))

defmodule Day3Tests do
use ExUnit.Case

  test "part 1 - example" do
    assert Day3.run("example.txt") == 161
  end

  test "part 1 " do
    assert Day3.run("input.txt") == 187194524
  end

  test "part 2 - example" do
    assert Day3.run_part2("example2.txt") == 48
  end

  test "part 2 " do
    assert Day3.run_part2("input.txt") == 127092535
  end
end
