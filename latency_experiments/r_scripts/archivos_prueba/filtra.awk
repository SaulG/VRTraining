#!/usr/bin/awk -f

BEGIN{
    print "Fila uno"
    print $1
    print "Fila dos"
    print $2
    if ($1 == "Min.") {
	min = $2
    }
    if ($1 == "1stQu.") {
	c1 = $2
    }
    if ($1 == "Median") {
	med = $2
    }
    if ($1 == "3rdQu.") {
	c3 = $2
    }
    if ($1 == "Max.") {
	max = $2
    }
}
END {
    print "\t"c1"\t"min"\t"max"\t"c3"\t"med    
}
