ExUnit.start()
Code.require_file(Path.join(__DIR__, "../2/rednosed_reports.exs"))


defmodule RedNosedReportsTest do
use ExUnit.Case

  test "part 1 - example" do
    assert RedNosedReports.run("example.txt") == 2
  end

  test "part 1 " do
    assert RedNosedReports.run("input.txt") == 379
  end

  test "part 2 - example" do
    assert RedNosedReports.run_part2("example.txt") == 4
  end

  test "part 2 " do
    assert RedNosedReports.run_part2("input.txt") == 430
  end
end
