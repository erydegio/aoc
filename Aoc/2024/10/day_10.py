def read_file(path):
    with open(path, 'r') as file:
        return [[int(y) for y in x.strip()] for x in file.readlines()]


def get_starts(data):
    start_points = []
    for x in range(len(data)):
        for y in range(len(data[0])):
            if data[x][y] == 0:
                start_points.append((x, y))
    return start_points


def get_next_points(x, y, data):
    directions = [(-1, 0), (1, 0), (0, -1), (0, 1)]
    next_points = []
    for dx, dy in directions:
        nx, ny = x + dx, y + dy
        if 0 <= nx < len(data) and 0 <= ny < len(data[0]):
            next_points.append((nx, ny))
    return next_points


def check_paths(start, input_data, valid_paths):
    next_paths = get_next_points(start[0], start[1], input_data)
    for coords in next_paths:
        if input_data[coords[0]][coords[1]] - input_data[start[0]][start[1]] != 1:
            continue

        if input_data[coords[0]][coords[1]] == 9 and input_data[start[0]][start[1]] != 9:
            valid_paths.append(coords)
        else:
            valid_paths = check_paths(coords, input_data, valid_paths)

    return valid_paths


def day_10_part_1(data, start_points):
    trail_count = 0
    end_points = []

    for start in start_points:
        paths = check_paths(start, data, [])
        end_points.extend(paths)
        trail_count += len(set(paths))

    return trail_count, end_points

def day_10_part_2(end_points):
    return len(end_points)