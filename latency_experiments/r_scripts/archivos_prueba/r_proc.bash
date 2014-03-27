Rscript summary_latency.R
sed 's/"//g' latencias.txt > latencias.sed
sed 's/ //g' latencias.sed > latencias.sed.filtro
./filtra.awk -F ":" latencias.sed.filtro > latencias.awk
