#formato de archivo de salida
set term postscript eps color

#el ancho de las graficas
set boxwidth 0.2 absolute

#rango del eje x
set xrange [ 0 : 6 ]

#nombre de etiquetas en un cierto valor
set xtics ("A" 1, "B" 2)

#para quitar leyendas en las graficas
set key off

#archivo de salida en el que se genera la grafica
set output "prueba.eps"

# box min whisker min whisker high box high
#  tomando los valores de prueba3.dat del 1st qu., minimo, maximo, 3rd qu.
#luego tenemos el 1st qu.
#despues la mediana
plot 'prueba3.dat' using 1:3:2:7:6 with candlesticks whiskerbars lt -1 lw 2 lc rgb "#aaaaaa", \
     '' using 1:3:3:3:3 with candlesticks lt -1 lw 2 lc rgb "#000000", \
     '' using 1:4:4:4:4 with candlesticks lt -1 lw 2 lc rgb "#666666"