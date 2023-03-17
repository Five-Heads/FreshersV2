import { Observable, Subject, takeUntil, take } from "rxjs";

export function untilComplete<T>(observable: Observable<T>){
    return observable.pipe(take(1));
}

export interface IDisposable {
    dispose: () => void;
}

export class Disposable implements IDisposable {
    private destroying$: Subject<void> = new Subject<void>();

    protected untilDispose<T>(observable: Observable<T>): Observable<T> {
        return observable.pipe(takeUntil(this.destroy$));
    }

    protected untilComplete<T>(observable: Observable<T>): Observable<T> {
        return this.untilDispose(untilComplete(observable));
    }

    protected get destroy$(): Observable<void> {
        return this.destroying$;
    }

    public dispose(): void {
        this.destroying$.next();
        this.destroying$.complete();
    }
}