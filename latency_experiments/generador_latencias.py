from random import randint
from subprocess import call

NOMBRE_ARCHIVO_LATENCIAS = 'latencias.dat'

def generaDatos(cantidad_de_datos, cantidad_de_etapas):    
    f = open(NOMBRE_ARCHIVO_LATENCIAS, 'w')
    randint(1,5)
    for x in xrange(cantidad_de_datos):
        for y in xrange(cantidad_de_etapas):
            f.write(str(randint(1,5))+' ')
        f.write('\n')
    f.close()
    
def main():
    cantidad_de_datos = 100
    cantidad_de_etapas = 5
    print cantidad_de_datos
    generaDatos(cantidad_de_datos, cantidad_de_etapas)
main()
