# sistemare codice duplicato

def parse_grid():
    with open("C:\\Users\\eryde\\OneDrive\\Desktop\\aoc\\day8\\input.txt") as input:
        data = [[Tree(int(number)) for number in line.strip()] for line in input.readlines()]  
    return data

def top_view(input, side):
    visible = 0

    for y in range(1,len(side) -1): # 11 21 31
        higher_tree = side[y].high
       
        for x in range(1,len(input)-1):
            tree = input[x][y]
            
            if tree.high > higher_tree:
                higher_tree = tree.high
                if tree.is_checked == False:
                    visible +=1
                    tree.is_checked = True
    return visible

def bottom_view(input, side):
    visible = 0

    for y in range(1,len(side) -1):
        higher_tree = side[y].high
     
        for x in range(len(input)-2, 0, -1): #31 21 11
            tree = input[x][y]

            if tree.high > higher_tree:
                higher_tree = tree.high
                if tree.is_checked == False:
                    visible +=1
                    tree.is_checked = True
    return visible

def left_view(input, side):
    visible = 0

    for n in range(1,len(side) -1):
        higher_tree = side[n].high #int
       
        for n2 in range(1,len(input)-1):
            tree = input[n][n2]

            if tree.high > higher_tree:
                higher_tree = tree.high
                if tree.is_checked == False:
                    visible +=1
                    tree.is_checked = True

    return visible

def right_view(input, side):
    visible = 0

    for n in range(1,len(side) -1):
        higher_tree = side[n].high #int

        for n2 in range(len(input)-2, 0, -1):
            tree = input[n][n2]
            
            if tree.high > higher_tree:
                higher_tree = tree.high
                if tree.is_checked == False:
                    visible +=1
                    tree.is_checked = True

    return visible


def res1():
    input = parse_grid()
    top = input[0]
    bottom = input[-1]
    left = [n_list[0] for n_list in input]
    right = [n_list[-1] for n_list in input]

    top_visible = top_view(input, top) #2
    bottom_visible = bottom_view(input, bottom) #1
    left_visible = left_view(input, left)
    right_visible = right_view(input, right)
    tot_border = len(top) + len(bottom) + len(left) + len(right) -4

    return top_visible + bottom_visible + left_visible + right_visible + tot_border

def res2():
    input = parse_grid()
    high = len(input[0]) #top bottom
    width = len([n_list[0] for n_list in input]) #left right


    highest_scenic_score = 0
    # ogni albero interno calcola scneic store,
    # loop alberi interni,
    for x in range(1, high -1): # 11 21 31
        for y in range(1, width -1):

            tree =input[x][y]
            up = tree.look_up(x, y, input)
            down = tree.look_down(x, y, input)
            left = tree.look_left(x, y, input)
            right = tree.look_right(x,y,input)

            scenic_score = up * down * left * right

            if scenic_score > highest_scenic_score:
                highest_scenic_score = scenic_score
        
    print(highest_scenic_score)

          


        #  di ognuno ritorno calc scenic store
    # se res calc scenic store + alto di highest_scenic-store lo sostituisce


class Tree:

    is_checked = False
    scenic_score= 0 

    def __init__(self, n):
        self.high = n

    def look_left(this, x, y, field):
        #x negativo
        score = 0
        for index in range(x-1, -1,-1):
            other_tree = field[index][y].high

            if this.high <= other_tree:
                score +=1
                return score
            else:
                score +=1
        
        return score

        return

    def look_down(this, x, y, field):
        # y in negativo
        score = 0
        
        for index in range(y-1, -1, -1):
            other_tree = field[x][index].high

            if this.high <= other_tree:
                score +=1
                return score
            else:
                score +=1
        return score

    def look_right(this, x, y, field):
        #x positivo
        score = 0
        width = len([n_list[0] for n_list in input]) 

        for index in range(x+1, width):
            other_tree = field[index][y].high

            if this.high <= other_tree:
                score +=1
                return score
            else:
                score +=1
        
        return score

    def look_up(this, x, y, field):
        #scorre y in pisitivo
        score = 0
        field_high = len(field[0]) 
        
        for index in range(y+1, field_high):
            other_tree = field[x][index].high

            if this.high <= other_tree:
                score +=1
                return score
            else:
                score +=1
        
        return score

input = parse_grid()
res1()
res2()