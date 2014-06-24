#!/bin/bash

#Corre script en R para obtener maximos, minimos, mediana y promedio
Rscript summary_latency.R

#primero se eliminan las comillas, luego los dos puntos y por ultimo Qu.
#Despues awk imprime por cada segunda columna:
#
# Min. 3.00
# 1st 7.00
# Median 11.00
# Mean 11.64
# 3rd 17.00
# Max. 20.00
# 
# Ej. 
#   $2 los valores de  3.00, 7.00, 11.00, 11.64, 17.00, 20.00
#
# se guardan en prueba2.dat
sed 's/\"//g' latencias.txt | sed 's/://g' | sed 's/Qu./ /g' | awk '{print $2"\t"$4"\t"$6"\t"$8"\t"}' > prueba2.dat

#Luego itera las cinco columnas del archivo prueba2.dat y transpone la matriz,
#agrega un contador en la primer columna de la nueva matriz que ha sido transpuesta

for i in 0 1 2 3; do awk -v c=$i '{print $c}' < prueba2.dat | awk '{a = a$1" "}END{print a}'; done | awk 'BEGIN{c=1}{print c" "$0; c++}' > prueba3.dat

# remueve el archivo viejo prueba.eps
rm prueba.eps
# corre el script de gnuplot con los datos generados anteriormente
gnuplot prueba.plot 
# abre el archivo generado por gnuplot
open prueba.eps
# obtiene las latencias totales de cada una de las etapas
# y los manda a un nuevo archivo llamado totales
awk '{print $4}' < latencias.dat > totales.dat
#remueve el archivo viejo de totales.eps
rm totales.eps
#corre el script de gnuplot con los datos generados de los totales.dat
gnuplot totales.plot
#abre el archivo generado por gnuplot
open totales.eps
