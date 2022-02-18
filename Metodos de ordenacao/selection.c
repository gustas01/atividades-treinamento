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



int encontraMenor(int inicio, int fim, int vet[]){
    int menor = inicio;
    for (int i = inicio; i < fim; i++){
            if (vet[i] < vet[menor])
                menor = i;
        
    }
    return menor;
}


void selecao(int tam, int vet[]){
    int marcador = 0, menor, aux;
    while (marcador < tam-1){
        menor = encontraMenor(marcador+1, tam, vet);
        if (vet[menor] < vet[marcador]){
            aux = vet[menor];
            vet[menor] = vet[marcador];
            vet[marcador] = aux;
        }
        marcador++;
    }
    
}



void main(){
    int tamanho = 100;
    
    clock_t t; //variável para armazenar tempo

    int *valores = (int*) malloc (tamanho * sizeof(int));
    int posicoesMenorMaior[2];

    geraNumerosAleatorios(tamanho, valores);
    

    t = clock(); //armazena tempo
    selecao(tamanho, valores);
    t = clock() - t; //tempo final - tempo inicial
    
    printf("Tempo de execucao: %lf", ((double)t)/((CLOCKS_PER_SEC))); //conversão para double
   
}