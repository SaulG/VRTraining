from random import randint
from subprocess import call

NOMBRE_ARCHIVO_LATENCIAS = 'latencias.dat'

def generaDatos(cantidad_de_datos, cantidad_de_etapas):    
    f = open(NOMBRE_ARCHIVO_LATENCIAS, 'w')
    randint(1,5)
    minimos = [3, 20, 4, 6, 30]
    maximos = [20, 60, 90, 34, 55]
    for x in xrange(cantidad_de_datos):
        total = 0
        for y in xrange(cantidad_de_etapas):
            valor = randint(minimos[y], maximos[y])
            f.write('%d ' % valor)
            total += valor
        f.write('%d\n' % total)
    f.close()
    
def main():
    cantidad_de_datos = 30000
    cantidad_de_etapas = 4
    generaDatos(cantidad_de_datos, cantidad_de_etapas)
main()
