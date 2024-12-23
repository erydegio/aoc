ExUnit.start()
Code.require_file(Path.join(__DIR__, "../19/day19.exs"))

defmodule Day10Tests do
use ExUnit.Case

  test "part 1 - example" do
    assert Day19.part_1("example.txt") == 6 #251
  end

  test "part 2 - example" do
    assert Day19.part_2("example.txt") == 16 #616957151871345
  end

end
