#include <stdio.h>
#include <stdlib.h> // necessário para as funções rand() e srand()
#include <time.h> //necessário para função time()
#include <string.h>

void geraNumerosAleatorios(int qtNumeros, int bolha[]){
    for(int i = 0; i < qtNumeros; i++)
        bolha[i] = (rand() % 10);
}


void quickSort(int vet[], int inicio, int fim){
    int posPivo;
    if (inicio < fim){
        posPivo = particiona(vet, inicio, fim);
        quickSort(vet, inicio, posPivo-1);
        quickSort(vet, posPivo+1, fim);
    }
    
}


int particiona(int vet[], int inicio, int fim){
    int pivo = vet[inicio];
    int pos = inicio;
    int aux;
    for(int i = inicio+1; i <= fim; i++){
        if (vet[i] < pivo){
            pos++;
            if(i != pos){
                aux = vet[i];
                vet[i] = vet[pos];
                vet[pos] = aux;
            }
        }
    
    }
    aux = vet[inicio];
    vet[inicio] = vet[pos];
    vet[pos] = aux;

    return pos;
}


void main(){
    int tamanho = 100000;
    
    clock_t t; //variável para armazenar tempo

    int *valores = (int*) malloc (tamanho * sizeof(int));
    

    geraNumerosAleatorios(tamanho, valores);


    t = clock(); //armazena tempo
    quickSort(valores, 0, tamanho-1);
    t = clock() - t; //tempo final - tempo inicial

    
    
    printf("Tempo de execucao: %lf", ((double)t)/((CLOCKS_PER_SEC))); //conversão para double
   
}