from sys import argv



def remapea(archivo, max, min):
    a = open(archivo,'r+w')
    for line in a:
        tiempos = line.split(',')
        
        
def obtiene_max_min(archivo):
    a = open(archivo, 'r')
    max = 0
    min = 999
    for line in a:
        tiempos = line.split(' ')
        for tiempo in tiempos:
            if int(tiempo) > max:
                max = int(tiempo)
            if int(tiempo) < min:
                min = int(tiempo)
    print "Maximo "+str(max)+" Minimo "+str(min)
    return max, min
        
def main():
    nombre_de_archivo = argv[1]
    max, min = obtiene_max_min(nombre_de_archivo)
    remapea(nombre_de_archivo, max, min)

main()
