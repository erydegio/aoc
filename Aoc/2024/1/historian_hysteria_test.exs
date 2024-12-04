ExUnit.start()
Code.require_file(Path.join(__DIR__, "../1/historian_hysteria.exs"))


defmodule HistorianHysteriaTest do
  use ExUnit.Case

  test "part 1 - example" do
    assert HistorianHysteria.run("example.txt") == 11
  end

  test "part 1 " do
    assert HistorianHysteria.run("input.txt") == 2192892
  end

  test "part 2 - example" do
    assert HistorianHysteria.run_part2("example.txt") == 31
  end

  test "part 2 " do
    assert HistorianHysteria.run_part2("input.txt") == 22962826
  end
end
