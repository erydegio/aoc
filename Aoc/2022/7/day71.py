 #find all of the directories with a total size of at most 100000

def add_directory(dir, line):
  if line not in dir.keys():
        dir[line] = 0
  return dir

def sum_values(dir):
    sum_values = 0
    for each_size in dir.values(): 
        if each_size <= 100000:
            sum_values += each_size
    return sum_values


def calc_directories():
    res = {}
    current_absolute_path = ''
    dir_stack = []
        
    with open('input.txt') as cmd_list:

        for line in cmd_list: 
            new_line = line.replace('\n', '').strip()

            if line.startswith("$ cd"):
                if '$ cd /' in new_line:
                    current_absolute_path = "/"
                    dir_stack = ["/"]
                    res = add_directory(res, current_absolute_path)     
                elif '$ cd ..' in new_line:
                    current_absolute_path += f"{line.split()[:-1]}/" if current_absolute_path != "/" else line.split()[-1]
                    del dir_stack[-1]         
                else: 
                    current_absolute_path = current_absolute_path + new_line.split()[-1] + '/'
                    dir_stack.append(current_absolute_path)
                    res = add_directory(res, current_absolute_path)
            
            if new_line[0].isdigit(): 
                size = int(new_line.split()[0])
                for i in dir_stack:
                  res[i] += size
    return res


def res1():
    result = sum_values(calc_directories())
    print(f"Result 1: {result}") # 1583951
    return result

def res2(dir):
    total_space = 70000000
    space_needed = 30000000
    root = "/"
    space_used = dir[root] #total space used
    space_to_free = space_needed - (total_space - space_used)
    sizes = [size for size in dir.values() if size >= space_to_free]

    print(f"Result 2: {sorted(sizes)[0]}")

res1()
res2(calc_directories())