ExUnit.start()
Code.require_file(Path.join(__DIR__, "../3/tobbogan_trajectory.exs"))

defmodule TobogganTrajectoryTest do
  use ExUnit.Case

  test "part 1 - example" do
    assert TobogganTrajectory.run("example.txt") == 7
  end

  test "part 1 " do
    assert TobogganTrajectory.run("input.txt") == 292
  end

  test "part 2 " do
    assert TobogganTrajectory.run2("example.txt") == 2
  end
end
