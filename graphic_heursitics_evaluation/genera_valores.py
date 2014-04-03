from random import random

HEURISTICAS = 12
EVAL = [1,4]
FILAS = 6
def genera_clasificaciones_evaluacion_heuristica():
    for x in xrange(FILAS):
        fila = ''
        for y in xrange(HEURISTICAS):
            fila+= str(int( (EVAL[1]+1) * random() ))+'\t'
        print fila
    
def main():
    genera_clasificaciones_evaluacion_heuristica()

main()
