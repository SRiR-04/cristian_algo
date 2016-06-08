#include <stdlib.h>
#include <stdio.h>
#include <math.h>
#include <time.h>
#include "mpi.h"
#include <inttypes.h>

int main ( int argc, char *argv[] );

/******************************************************************************/
long getTime(void)
{
    long            ms; // Milliseconds
    time_t          s;  // Seconds
    struct timespec spec;

    clock_gettime(CLOCK_REALTIME, &spec);

    s  = spec.tv_sec;
    ms = round(spec.tv_nsec / 1.0e6); // Convert nanoseconds to milliseconds
    printf("nano %ld\n", spec.tv_nsec);
    printf("Current time: %"PRIdMAX".%03ld sec\n",
           (intmax_t)s, ms);
    return ms;

}

int main ( int argc, char *argv[] )
{
  int id; // rank
  int p;  // size

  MPI_Init (&argc, &argv);

  MPI_Comm_rank (MPI_COMM_WORLD, &id);

  MPI_Comm_size (MPI_COMM_WORLD, &p);

  int number;
  long t1, t2, receivedServerTime;

  if (id == 0) {
    number = -1;
    t1 = getTime();
    // char* c_time_string;
    // c_time_string = ctime(&t1);
    // printf("Current time is %s", c_time_string);
    MPI_Send(&number, 1, MPI_INT, 1, 0, MPI_COMM_WORLD);
  } else if (id == 1) {
    MPI_Recv(&number, 1, MPI_INT, 0, 0, MPI_COMM_WORLD, MPI_STATUS_IGNORE);
    printf("Process 1 received number %d from process 0\n", number);
    
    long serverTime = getTime();
    MPI_Send(&serverTime, 1, MPI_INT, 0, 0, MPI_COMM_WORLD);
  }
  
  if (id == 0) {
    MPI_Recv(&receivedServerTime, 1, MPI_INT, 1, 0, MPI_COMM_WORLD, MPI_STATUS_IGNORE);
    t2 = getTime();
    long computedTime = receivedServerTime + t2 - t1;
    long diff = t2 - t1;   
    printf("the difference is %ld\n", diff);
  }

  MPI_Finalize ();

  return 0;
}
