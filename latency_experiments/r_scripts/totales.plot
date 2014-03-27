set term postscript eps color

set key off
set xlabel 'Tiempo (ms)'
set ylabel 'Frecuencia'
set output "totales.eps"

binwidth = 25
set boxwidth binwidth
bin(x,width) = width * floor(x / width) + binwidth / 2.0
set style fill transparent solid 0.7 border -1


plot 'totales.dat' using (bin($1, binwidth)):(1.0) smooth freq with boxes ls -1 lc "#cccccc"
