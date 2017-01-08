// GPL v3 License
// 
// Copyright (c) 2016-2017 Bismur Studios Ltd.
// Copyright (c) 2016-2017 Ioannis Giagkiozis
// 
// ReaderWriterLockSlimExtensions.cs is part of Crystal AI.
//  
// Crystal AI is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//  
// Crystal AI is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Crystal AI.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Threading;


namespace Crystal {

  public static class ReaderWriterLockSlimExtensions {
    public static IDisposable Read(this ReaderWriterLockSlim @this) {
      return new ReadLockToken(@this);
    }

    public static IDisposable Write(this ReaderWriterLockSlim @this) {
      return new WriteLockToken(@this);
    }

    sealed class ReadLockToken : IDisposable {
      ReaderWriterLockSlim _sync;

      public void Dispose() {
        if(_sync != null) {
          _sync.ExitReadLock();
          _sync = null;
        }
      }

      public ReadLockToken(ReaderWriterLockSlim sync) {
        _sync = sync;
        sync.EnterReadLock();
      }
    }

    sealed class WriteLockToken : IDisposable {
      ReaderWriterLockSlim _sync;

      public void Dispose() {
        if(_sync != null) {
          _sync.ExitWriteLock();
          _sync = null;
        }
      }

      public WriteLockToken(ReaderWriterLockSlim sync) {
        _sync = sync;
        sync.EnterWriteLock();
      }
    }
  }

}