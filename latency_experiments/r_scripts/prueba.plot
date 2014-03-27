set term postscript eps color
set boxwidth 0.2 absolute
set xrange [ 0 : 6 ]
set xtics ("A" 1, "B" 2)
set key off
set output "prueba.eps"
# box min whisker min whisker high box high
plot 'prueba3.dat' using 1:3:2:7:6 with candlesticks whiskerbars lt -1 lw 2 lc rgb "#aaaaaa", \
     '' using 1:3:3:3:3 with candlesticks lt -1 lw 2 lc rgb "#000000", \
     '' using 1:4:4:4:4 with candlesticks lt -1 lw 2 lc rgb "#666666"