#include <stdio.h>
#include <stdlib.h> // necessário para as funções rand() e srand()
#include <conio.h>
#include <time.h> //necessário para função time()
#include <string.h>


void geraNumerosAleatorios(int qtNumeros, int bolha[]){
  
   
    for(int i = 0; i < qtNumeros; i++)
        bolha[i] = (rand() % 10);

}


void bolhaInteligente(int vet[], int tam){
    int aux, ordenado = 0;
    
    for(int i = 0; i < tam; i++){
        ordenado = 0;
        for(int j = 0; j < tam - 1 - i; j++){
            if (vet[j+1] < vet[j]){
                aux = vet[j];
                vet[j] = vet[j+1];
                vet[j+1] = aux;
            
            ordenado++;
                
            }
            
        }
        if(ordenado == 0)
            break;
        
    }
}

void main(){
    int tamanho = 100000;
    
    clock_t t; //variável para armazenar tempo

    int *valores = (int*) malloc (tamanho * sizeof(int));
    

    geraNumerosAleatorios(tamanho, valores);

    t = clock(); //armazena tempo
    bolhaInteligente(valores, tamanho);
    t = clock() - t; //tempo final - tempo inicial

     printf("Tempo de execucao: %lf", ((double)t)/((CLOCKS_PER_SEC))); //conversão para double
    
}