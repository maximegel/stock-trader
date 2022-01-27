package main

import "testing"

func TestHello(t *testing.T) {
	expected := "Hello, World!"
	received := Hello("World")
	if received != expected {
		t.Fatalf(`expected %v, but received %v`, expected, received)
	}
}
