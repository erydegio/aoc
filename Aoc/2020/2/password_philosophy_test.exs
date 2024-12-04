ExUnit.start()
Code.require_file(Path.join(__DIR__, "../2/password_philosophy.exs"))

defmodule PasswordPhilosophyTest do
  use ExUnit.Case

  test "part 1 - example" do
    assert PasswordPhilosophy.run("example.txt") == 2
  end

  test "part 1 " do
    assert PasswordPhilosophy.run("input.txt") == 493
  end

  test "part 2 - example" do
    assert PasswordPhilosophy.run2("example.txt") == 1
  end

  test "part 2 " do
    assert PasswordPhilosophy.run2("input.txt") == 593
  end
end
