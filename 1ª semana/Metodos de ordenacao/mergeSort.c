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


void mergeSort(int vet[], int inicio, int fim){
    int meio;
    if(inicio < fim){
        meio = (inicio+fim)/2;
        mergeSort(vet, inicio, meio);
        mergeSort(vet, meio+1, fim);
        merge(vet, inicio, meio, fim);
    }
}


void merge(int vet[], int inicio, int meio, int fim){
    int marcadorV1 = inicio, marcadorV2 = meio+1;
    int vetAux[fim-inicio+1];

    int i = 0, k;

    while ((marcadorV1 <= meio) && (marcadorV2 <= fim)){
        if(vet[marcadorV1] < vet[marcadorV2]){
            vetAux[i] = vet[marcadorV1];
            marcadorV1++;
        }else{
            vetAux[i] = vet[marcadorV2];
            marcadorV2++;
        }
        i++;
    }

    //copiando o resto do vetor 1 ou 2 para o vetor auxiliar
    while (marcadorV1 <= meio){
        vetAux[i] = vet[marcadorV1];
        i++;
        marcadorV1++;
    }

    while (marcadorV2 <= fim){
        vetAux[i] = vet[marcadorV2];
        i++;
        marcadorV2++;
    }

//copiando o vetor auxiliar para o vetor original
    for ( i = inicio; i <= fim; i++)
        vet[i] = vetAux[i-inicio];


}


void imprimeVetor(int tam, int vet[]){
    for(int i = 0; i < tam; i++){
        printf("%i ", vet[i]);
    }
    printf("\n");
}

void main(){
    int tamanho = 10;
    
    clock_t t; //variável para armazenar tempo

    int *valores = (int*) malloc (tamanho * sizeof(int));
    int posicoesMenorMaior[2];

    geraNumerosAleatorios(tamanho, valores);

    t = clock(); //armazena tempo
    mergeSort(valores, 0, tamanho-1);
    t = clock() - t; //tempo final - tempo inicial

    
    
    printf("Tempo de execucao: %lf", ((double)t)/((CLOCKS_PER_SEC))); //conversão para double
   
}