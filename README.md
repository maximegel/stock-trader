# Stock Trader

Simple CQRS implementation of a stock trading application.

## Troubleshooting

### Tilt: Error writing api configs

```console
$ tilt up
Error: writing tilt api configs: open /home/vscode/.tilt-dev/config.lock: file exists
```

See issue [here](https://github.com/tilt-dev/tilt/issues/4814).

#### Solution

Delete the `.tilt-dev` directory using:

```console
rm -rf ~/.tilt-dev
```

---

## References

- [Event storming the stock trader domain](https://developer.ibm.com/tutorials/reactive-in-practice-1/)
