#include <stdio.h>
#include <stdlib.h> // necessário para as funções rand() e srand()
#include <time.h> //necessário para função time()
#include <string.h>
// #include "selecao.h"
// #include "selecao.c"

void geraNumerosAleatorios(int qtNumeros, int bolha[]){
    for(int i = 0; i < qtNumeros; i++)
        bolha[i] = (rand() % 10);
}



void insercao(int tam, int vet[]){
    int marcador, aux, pos;
    for(marcador = 1; marcador < tam; marcador++){
        pos = marcador-1;
        aux = vet[marcador];
        while((aux < vet[pos]) && (pos >= 0)){
            vet[pos+1] = vet[pos];
            pos -=1;
        }
        vet[pos+1] = aux;
    }
}


void main(){
    int tamanho = 100000;
    
    clock_t t; //variável para armazenar tempo

    int *valores = (int*) malloc (tamanho * sizeof(int));
    int posicoesMenorMaior[2];

    geraNumerosAleatorios(tamanho, valores);
    

    t = clock(); //armazena tempo
    insercao(tamanho, valores);
    t = clock() - t; //tempo final - tempo inicial
    
    
    printf("Tempo de execucao: %lf", ((double)t)/((CLOCKS_PER_SEC))); //conversão para double
   
}