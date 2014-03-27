Rscript quantiles.R
sed 's/ //g' summary > stat
./parse.awk -F ":" -v hdr=${gamma} stat > fast.${gamma}.stat
