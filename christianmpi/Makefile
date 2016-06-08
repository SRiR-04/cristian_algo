# kompilator c
CCOMP = mpicc

# konsolidator
LINK = mpicc

MPIRUN = mpiexec 

# opcje optymalizacji:
# wersja do debugowania
# OPT = -g -DDEBUG -p
# wersja zoptymalizowana do mierzenia czasu
# OPT = -O3 -fopenmp -p

# pliki naglowkowe
#INC = -I../pomiar_czasu

# biblioteki
#LIB = -L../pomiar_czasu -lm
LIB = -lm

# zaleznosci i komendy

mpi: mpi.o
	$(LINK) $(OPT) mpi.o -o mpi $(LIB)

mpi.o: mpi.c
	$(CCOMP) -c $(OPT) mpi.c $(INC)

run:
	$(MPIRUN) -np 2 ./mpi

clean:
	rm -f *.o
