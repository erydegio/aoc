def find_continuous_areas(coordinates):
    def search(x, y, visited, group):
        stack = [(x, y)]
        while stack: # ad un certo punto nn ci sarà + nulla in nessuna direzione
            cx, cy = stack.pop()
            if (cx, cy) not in visited:
                visited.add((cx, cy))
                group.append((cx, cy))
                for nx, ny in [(cx-1, cy), (cx+1, cy), (cx, cy-1), (cx, cy+1)]:
                    if (nx, ny) in coords and (nx, ny) not in visited:
                    # il nuovo percorso deve essere tra quelli delle mie cordinateper quella lettera e
                    # non in visited
                        stack.append((nx, ny))

    areas = {}
    for letter, coords in coordinates.items():
        visited = set()
        areas[letter] = []
        for x, y in coords:
            if (x, y) not in visited:
                group = []
                search(x, y, visited, group)
                areas[letter].append(group)
    return areas


def parse_map(map_str):
    return [list(row) for row in map_str.strip().split('\n')]


def create_coordinate_dict(grid):
    coordinate_dict = {}

    for i, row in enumerate(grid.split('\n')):
        for j, char in enumerate(row):
            if char not in coordinate_dict:
                coordinate_dict[char] = []
            coordinate_dict[char].append((i, j))

    return coordinate_dict

def calculate_perimeter(area):
    perimeter = 0
    for x, y in area:
        # Controlla i quattro lati di ogni cella
        if (x-1, y) not in area:
            perimeter += 1
        if (x+1, y) not in area:
            perimeter += 1
        if (x, y-1) not in area:
            perimeter += 1
        if (x, y+1) not in area:
            perimeter += 1
    return perimeter


def calculate_areas(coordinate_dict):
    areas = {}
    for letter, groups in coordinate_dict.items():
        acc = 0
        for group in groups:
            acc +=  len(group) * calculate_perimeter(group)
        areas[letter] = acc
    return sum(areas.values())


def read_file_as_string(file_path):
    with open(file_path, 'r') as file:
        content = file.read()
    return content


content = read_file_as_string("example.txt")
coordinates2 = create_coordinate_dict(content) # dizionario cn tt le cordinate x quella letter
areas = find_continuous_areas(coordinates2) # trovo spazi continui per quella lettera
print(areas)
res = calculate_areas(areas)
print(res)


def create_edge_dict(area):
    edges = {}
    for x, y in area:
        edges[(x, y)] = {'up': True, 'down': True, 'dx': True, 'sx': True}

    for x, y in area:
        if (x-1, y) in area:
            edges[(x, y)]['sx'] = True
            edges[(x-1, y)]['dx'] = True
        if (x+1, y) in area:
            edges[(x, y)]['dx'] = True
            edges[(x+1, y)]['sx'] = True
        if (x, y-1) in area:
            edges[(x, y)]['up'] = True
            edges[(x, y-1)]['down'] = True
        if (x, y+1) in area:
            edges[(x, y)]['down'] = True
            edges[(x, y+1)]['up'] = True

    return edges

def find_sides(areas):

    # ho le cordinate di un poligono:
    edges_info = create_edge_dict(areas)
    #aggiorno le info sulle facce (se guardano verso l'esterno  ono)
    for x, y in areas:
        if (x - 1, y) not in areas:
            edges_info[(x,y)]['sx'] = False
        if (x + 1, y) not in areas:
            edges_info[(x,y)]['dx'] = False
        if (x, y - 1) not in areas:
            edges_info[(x,y)]['down'] = False
        if (x, y + 1) not in areas:
            edges_info[(x,y)]['up'] = False

    sides = 0
    visited = set()
    for coords, edges in edges_info.items(): # scorro le cordinate
        x = coords[0]
        y = coords[1]

        print(f"Coordinata {x}, {y}:")

        for direction ,value in edges.items(): # scorro le direzioni della cordinata
            # se nei visited ci sono lati vicini che guardano nella stessa direzione a false?
            if value is False:
                if both_neighbours_are_sides_and_not_visited(direction, x, y, visited, edges_info, area):
                    sides += 1

        visited.add(coords)
    print(sides)
    return sides



def both_neighbours_are_sides_and_not_visited(direction, x, y, visited, edges_info, area):
    cross_directions = {"dx": [(x, y + 1), (x, y - 1)],
                        "up": [(x + 1, y), (x - 1, y)],
                        "down": [(x + 1, y), (x - 1, y)],
                        "sx": [(x, y + 1), (x, y - 1)]}
    # check cross directions###
    cross_dir1 = (cross_directions[direction][0]) # (nx,ny)
    cross_dir2 = cross_directions[direction][1]

    not_facing_same_area = []   #devo capire i campi cross direzione quanti sono nell'area:
    count_not_facing = 0

    if cross_dir1 in area  and edges_info[cross_dir1][direction] is False: not_facing_same_area.append(cross_dir1)
    if cross_dir2 in area and edges_info[cross_dir2][direction] is False : not_facing_same_area.append(cross_dir2)

    if not not_facing_same_area: # se lato singolo
        if (x, y) not in visited: return True #lo conto solo se non è stato visitato
        else: return False
    else :
       for cross_dir in not_facing_same_area:
            if cross_dir in area:
                if cross_dir not in visited and edges_info[cross_dir][direction] is False:  # se non è stato visitato ed è pezzo di un lato
                    count_not_facing += 1
            return count_not_facing == len(not_facing_same_area) # se entrambi non sono stati contati e sono a false conto

# Esempio di utilizzo
area = [(0, 0), (0, 1), (0, 2), (0, 3), (0, 4), (1, 0), (2, 0), (2, 1), (2, 2), (2, 3), (2, 4), (3, 0), (4, 0), (4, 1), (4, 2), (4, 3), (4, 4)]
sides = find_sides(area)
print(sides)



