#!/bin/bash
echo "Comenzando..."
rm promedios_datos_evaluacion.dat
for i in {1..12}; do
    awk '{sum+= $'${i}';} END{print sum/6}' < datos_evaluacion.dat >> promedios_datos_evaluacion.dat;
done
echo "Acabe.";
