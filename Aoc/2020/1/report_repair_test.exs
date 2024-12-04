ExUnit.start()
Code.require_file(Path.join(__DIR__, "../1/report_repair.exs"))


defmodule ReportRepairTest do
  use ExUnit.Case

  test "part 1 - example" do
    assert ReportRepair.run("example.txt") == 514579
  end

  test "part 1 " do
    assert ReportRepair.run("input.txt") == 1005459
  end

  test "part 2 - example" do
    assert ReportRepair.run_part2("example.txt") == 241861950
  end

  test "part 2 " do
    assert ReportRepair.run_part2("input.txt") == 92643264
  end
end
