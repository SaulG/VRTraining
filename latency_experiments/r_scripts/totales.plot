set term postscript eps color

set key off
set xlabel 'Tiempo (ms)'
set ylabel 'Frecuencia'
set output "totales.eps"

#la cantidad de brincos por los que se grafican
#los valores obtenidos del archivo totales dat
binwidth = 25
# se asigna el valor
set boxwidth binwidth
#funcion que realiza la logica de los brincos
#segun el ancho total
bin(x,width) = width * floor(x / width) + binwidth / 2.0
#estilo de la grafica
set style fill transparent solid 0.7 border -1

#crea la grafica tomando la funcion para colocar el ancho con los estilos y cajas
# con un tono gris
plot 'totales.dat' using (bin($1, binwidth)):(1.0) smooth freq with boxes ls -1 lc "#cccccc"
