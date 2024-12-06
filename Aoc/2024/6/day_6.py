
DIRECTIONS = [(0, -1), (1, 0), (0, 1), (-1, 0)]


def day_6_part_1():
    rows = read_file()
    current_direction = 0
    start_x, start_y = find_start_position(rows)
    current_position = (start_x, start_y)
    visited_positions = {current_position}

    while True:
        dx, dy = DIRECTIONS[current_direction]
        new_x, new_y = current_position[0] + dx, current_position[1] + dy

        if 0 <= new_x < len(rows[0]) and 0 <= new_y < len(rows):
            if rows[new_y][new_x] == '#':
                current_direction = (current_direction + 1) % 4
            else:
                current_position = (new_x, new_y)
                visited_positions.add(current_position)
        else:
            break

    return visited_positions


def find_start_position(rows):
    start_x, start_y = None, None
    for y, row in enumerate(rows):
        for x, cell in enumerate(row):
            if cell == '^':
                start_x, start_y = x, y
    return start_x, start_y


def read_file():
    with open("input.txt", 'r') as file:
        return [list(row.strip()) for row in file]


def day_6_part_2(visited_positions):
    rows = read_file()
    start_x, start_y = find_start_position(rows)
    loop_count = 0

    for point in visited_positions - {(start_x, start_y)}:
        current_direction = 0
        temp_rows = [row[:] for row in rows]
        temp_rows[point[1]][point[0]] = '#'

        temp_visited = set()
        current_position = (start_x, start_y, current_direction)

        temp_visited.add(current_position)

        while True:
            dx, dy = DIRECTIONS[current_direction]
            new_x, new_y = current_position[0] + dx, current_position[1] + dy

            if 0 <= new_x < len(temp_rows[0]) and 0 <= new_y < len(temp_rows):
                if temp_rows[new_y][new_x] == '#':
                    current_direction = (current_direction + 1) % 4

                    if (current_position[0], current_position[1], current_direction) in temp_visited:
                        loop_count += 1
                        break
                else:
                    current_position = (new_x, new_y, current_direction)
                    if current_position in temp_visited:
                        loop_count += 1
                        break
                    temp_visited.add(current_position)
            else:
                break
    return loop_count


result1 = day_6_part_1()
result2 = day_6_part_2(result1)

print(f"Part 1:{len(result1)}")
print(f"Part 2:{result2}")