//using ServiceStack.Redis;
//using ShenNong.Redis.Extensions;
//using System;
//using System.Threading;
//using System.Threading.Tasks;

//namespace ShenNong.Redis
//{
//    public class RedisLock
//    {
//        private RedisClient redisClient;
//        //private string redisKey;
//        public static int LOCK_EXPIRATION_INTERVAL_SECONDS = 30;
//        protected double internalLockLeaseTime = TimeSpan.FromSeconds(LOCK_EXPIRATION_INTERVAL_SECONDS).TotalMilliseconds;
//        readonly Guid id;

//        //public Task<long> tryAcquireOnceAsync () 
//        //{
//        //    acquiredFuture = tryLockInnerAsync(waitTime, leaseTime, unit, threadId, RedisCommands.EVAL_NULL_BOOLEAN);
//        //}
//        private int lockLeaseTime = 10;
//        protected RedisLock(Guid id)
//        {
//            //super(commandExecutor, name);
//            //this.commandExecutor = commandExecutor;
//            this.id = id;
//        }

//        //        public void lockInterruptibly(long leaseTime = -1, TimeSpan unit)
//        //        {
//        //            Long ttl = tryAcquire(leaseTime, unit);
//        //        // lock acquired
//        //        if (ttl == null) {
//        //            return;
//        //        }

//        //        long threadId = Thread.currentThread().getId();
//        //        Future<RedissonLockEntry> future = subscribe(threadId);
//        //        get(future);

//        //        try {
//        //            while (true) {
//        //                ttl = tryAcquire(leaseTime, unit);
//        //                // lock acquired
//        //                if (ttl == null) {
//        //                    break;
//        //                }

//        //                // waiting for message
//        //                if (ttl >= 0) {
//        //                    getEntry(threadId).getLatch().tryAcquire(ttl, TimeUnit.MILLISECONDS);
//        //    } else {
//        //                    getEntry(threadId).getLatch().acquire();
//        //}
//        //            }
//        //        } finally
//        //{
//        //    unsubscribe(future, threadId);
//        //}
//        //}

//        //private long tryAcquire(long leaseTime, TimeSpan unit)
//        //{
//        //    return get(tryAcquireAsync(leaseTime, unit, Thread.CurrentThread.ManagedThreadId));
//        //}

//        private RedisText tryAcquireOnceAsync(string key,long leaseTime, TimeSpan unit, long threadId)
//        {
//            if (leaseTime != -1)
//            {
//                return tryLockInner(key, leaseTime, unit, threadId);
//            }
//            // RedisCommands.EVAL_NULL_BOOLEAN
//            // TimeUnit.SECONDS
//            RedisText ttlRemainingFuture = tryLockInner(key, LOCK_EXPIRATION_INTERVAL_SECONDS, unit, threadId);

//            //    ttlRemainingFuture.addListener(new FutureListener<Boolean>() {
//            //        @Override
//            //        public void operationComplete(Future<Boolean> future) throws Exception {
//            //        if (!future.isSuccess())
//            //        {
//            //            return;
//            //        }

//            //        Boolean ttlRemaining = future.getNow();
//            //        // lock acquired
//            //        if (ttlRemaining)
//            //        {
//            //            scheduleExpirationRenewal();
//            //        }
//            //    }
//            //});
//            //    return ttlRemainingFuture;
//        }

//        // RedisCommands.EVAL_NULL_BOOLEAN
//        private RedisText tryLockInner(string key, long leaseTime, TimeSpan unit, long threadId)
//        {
//            //internalLockLeaseTime = unit.toMillis(leaseTime);
//            string internalLockLeaseTime = string.Empty;
//            string command2 = "if (redis.call('exists', KEYS[1]) == 0) then " +
//                          "redis.call('hset', KEYS[1], ARGV[2], 1); " +
//                          "redis.call('pexpire', KEYS[1], ARGV[1]); " +
//                          "return nil; " +
//                      "end; " +
//                      "if (redis.call('hexists', KEYS[1], ARGV[2]) == 1) then " +
//                          "redis.call('hincrby', KEYS[1], ARGV[2], 1); " +
//                          "redis.call('pexpire', KEYS[1], ARGV[1]); " +
//                          "return nil; " +
//                      "end; " +
//                      "return redis.call('pttl', KEYS[1]);";
//            return redisClient.ExecLua(command2, LuaUtils.SingletonList(key), new string[2] { internalLockLeaseTime, GetLockName(threadId) });
//        }

//        string GetLockName(long threadId)
//        {
//            return id + ":" + threadId;
//        }
//    }
//}