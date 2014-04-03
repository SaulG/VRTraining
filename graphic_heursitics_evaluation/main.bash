#!/bin/bash

echo "> Iniciando generador de valores..."
python genera_valores.py > datos_evaluacion.dat
echo "> Iniciando obtencion de clasificaciones heuristicas..."
bash obtener_promedios_evaluacion.bash

#Gnuplot lista_heuristicas_prom.gpi
