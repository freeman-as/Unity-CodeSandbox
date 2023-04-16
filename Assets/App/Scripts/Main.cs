using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


public class Main : MonoBehaviour
{
    private Subject<int> _numberSubject = new Subject<int>();
    public IObservable<int> NumberObservable => _numberSubject;


    // Start is called before the first frame update
    void Start()
    {
        //NumberObservable
        //    .Where(x => x > 0)
        //    .Subscribe(x => Debug.Log(x))
        //    .AddTo(this);


        //_numberSubject.OnNext(1);
        //_numberSubject.OnNext(2);
        //_numberSubject.OnNext(-1);

        //StartCoroutine(ToObservable());

        Observable.FromCoroutine<int>(observer => CounterCoroutine(observer, 100))
            .Subscribe(x =>
            {
                Debug.Log($"{x}ïbåoâﬂ");
            })
            .AddTo(this);

        //_numberSubject.Subscribe(x =>
        //{
        //    Debug.Log($"{x}ïbåoâﬂ");
        //})
        //.AddTo(this);

        var ob = Observable.Create<int>(observer =>
        {
            Debug.Log("Start");

            for (var i = 0; i <= 100; i += 10)
            {
                observer.OnNext(i);
            }

            Debug.Log("Finished");
            observer.OnCompleted();
            return Disposable.Create(() =>
            {
                //èIóπéûÇÃèàóù
                Debug.Log("Dispose");
            });
        });

        ob.Subscribe(x =>
        {
            Debug.Log($"{x}ïbåoâﬂ");
        })
        .AddTo(this);
    }

    private IEnumerator CounterCoroutine(IObserver<int> observer, int limit)
    {
        var count = 0;
        while(count < limit)
        {
            count += 1;
            Debug.Log($"count: {count}");
            observer.OnNext(count);

            yield return new WaitForSeconds(0.5f);
        }
        observer.OnCompleted();
    }

    private void Timer()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
