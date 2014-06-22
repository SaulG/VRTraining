from datetime import datetime
from sys import argv

FORMATO_FECHA = '%m %d %Y %H:%M:%S.%f'
def parseaTiempos(archivo):
    a = open(archivo, 'r')
    p = open(archivo+'-parseado','w')
    for line in a:
        fechas = line.split(',')
        contador = 0
        print fechas
        for fecha in fechas:
            contador = contador+1
            print fecha +' contador: '+str(contador)
            fecha = fecha.replace('\n','')
            aux = datetime.strptime(fecha, FORMATO_FECHA)
            aux = str(aux.microsecond/1000)
            p.write(aux)
            if len(fechas) != contador:
                p.write(',')
        p.write('\n')
    a.close()
    p.close()

def main():
    nombre_de_archivo = argv[1]
    print nombre_de_archivo
    parseaTiempos(nombre_de_archivo)

main()
