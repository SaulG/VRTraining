#!/bin/bash

sed 's/\"//g' latencias.txt | sed 's/://g' | sed 's/Qu./ /g' | awk '{print $2"\t"$4"\t"$6"\t"$8"\t"$10"\t"}' > prueba2.dat
for i in 1 2 3 4 5; do awk -v c=$i '{print $c}' < prueba2.dat | awk '{a = a$1" "}END{print a}'; done | awk 'BEGIN{c=1}{print c" "$0; c++}' > prueba3.dat
rm prueba.eps
gnuplot prueba.plot 
open prueba.eps
awk '{print $6}' < latencias.dat > totales.dat
rm totales.eps
gnuplot totales.plot
open totales.eps
