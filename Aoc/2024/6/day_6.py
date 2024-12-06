def day_6_part_1():
    rows = read_file()
    directions = [(0, -1), (1, 0), (0, 1), (-1, 0)]
    current_direction = 0
    start_x, start_y = find_start_position(rows)

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

    return visited_positions


def day6_part2(visited_positions):
    rows = read_file()
    start_x, start_y = find_start_position(rows)
    directions = [(0, -1), (1, 0), (0, 1), (-1, 0)]

    loop_count = 0

    for point in visited_positions - {(start_x, start_y)}:
        # Creare una copia della griglia originale
        temp_rows = [row[:] for row in rows]

        # Aggiungere un ostacolo al punto corrente
        temp_rows[point[1]][point[0]] = '#'

        # percorro la mappa
        # Calcolare il nuovo percorso
        new_visited = set()
        current_position = (start_x, start_y)
        new_visited.add(current_position)

    return loop_count




def find_start_position(rows):
    start_x, start_y = None, None
    for y, row in enumerate(rows):
        for x, cell in enumerate(row):
            if cell == '^':
                start_x, start_y = x, y
    return start_x, start_y


def read_file():
    with open("input.txt", 'r') as file:
        map_data = file.read()

    return [list(row.strip()) for row in map_data.split('\n')]


def count_loop_causing_points(visited_positions):
    rows = read_file()
    start_x, start_y = find_start_position(rows)
    directions = [(0, -1), (1, 0), (0, 1), (-1, 0)]
    loop_count = 0

    for point in visited_positions - {(start_x, start_y)}:

        # Creare una copia della griglia
        temp_rows = [row[:] for row in rows]
        current_direction = 0

        # Aggiungere un ostacolo al punto corrente
        temp_rows[point[1]][point[0]] = '#'

        # Calcolare il nuovo percorso
        new_visited = set()
        current_position = (start_x, start_y, current_direction)
        new_visited.add(current_position)

        while True:
            dx, dy = directions[current_direction]
            new_x, new_y = current_position[0] + dx, current_position[1] + dy

            if 0 <= new_x < len(temp_rows[0]) and 0 <= new_y < len(temp_rows):
                if temp_rows[new_y][new_x] == '#':
                    current_direction = (current_direction + 1) % 4

                    # Controllare se si è tornati al punto
                    if (current_position[0], current_position[1], current_direction) in new_visited:
                        loop_count += 1
                        break

                else:
                    current_position = (new_x, new_y, current_direction)

                    # Controllare se si è tornati al punto di ostacolo
                    if current_position in new_visited:
                        loop_count += 1
                        break
                    new_visited.add(current_position)
            else:
                break

    return loop_count

result1 = day_6_part_1()
result2 = count_loop_causing_points(result1)

# Chiamare la funzione dopo aver calcolato visited_positions
print(f"{len(result1)} - should be 5131")
print(f"Il numero di punti che causano un loop è: {result2}")

