def day_6():
    with open("input.txt", 'r') as file:
        map_data = file.read()

    rows = [list(row.strip()) for row in map_data.split('\n')]

    start_x, start_y = None, None
    for y, row in enumerate(rows):
        for x, cell in enumerate(row):
            if cell == '^':
                start_x, start_y = x, y

    directions = [(0, -1), (1, 0), (0, 1), (-1, 0)]
    current_direction = 0

    current_position = (start_x, start_y)
    visited_positions = {current_position}

    while True:
        dx, dy = directions[current_direction]
        new_x, new_y = current_position[0] + dx, current_position[1] + dy

        if 0 <= new_x < len(rows[0]) and 0 <= new_y < len(rows):
            if rows[new_y][new_x] == '#':  # if there's an obstacle
                current_direction = (current_direction + 1) % 4
            else:  # move forward
                current_position = (new_x, new_y)
                visited_positions.add(current_position)
        else:  # not in the mapped area
            break

    # in ogni punto in cui passa provo a mettere un ostacolo.
    # provo a farlo camminare se trovovuna cordinata (pattern per capire se gira su se stesso) allora è una ripetizione
    # sommo

    return len(visited_positions)


result = day_6()
print(f"{result}")
