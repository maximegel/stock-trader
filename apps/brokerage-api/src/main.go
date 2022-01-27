package main

import (
	"fmt"
	"net/http"
)

const (
	port = ":80"
)

func HelloWorld(w http.ResponseWriter, r *http.Request) {
	message := Hello("World")
	fmt.Fprintf(w, message)
}

func main() {
	http.HandleFunc("/", HelloWorld)
	http.ListenAndServe(port, nil)
}
