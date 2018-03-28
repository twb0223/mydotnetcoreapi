﻿using System;
using System.Collections.Generic;

namespace EventBus.Core
{
    /// <summary>
    /// Represents the event handler which delegates the event handling process to
    /// a given <see cref="Action{T}"/> delegated method.
    /// </summary>
    /// <typeparam name="TEvent">The type of the event to be handled by current handler.</typeparam>
    public sealed class ActionDelegatedEventHandler<TEvent> : IEventHandler<TEvent>,
        IEquatable<ActionDelegatedEventHandler<TEvent>> where TEvent : IEvent
    {
        #region Private Fields
        private readonly Action<TEvent> action;
        #endregion

        #region Ctor
        /// <summary>
        /// Initializes a new instance of <c>ActionDelegatedEventHandler{TEvent}</c> class.
        /// </summary>
        /// <param name="action">The <see cref="Action{T}"/> instance that delegates the event handling process.</param>
        public ActionDelegatedEventHandler(Action<TEvent> action)
        {
            this.action = action;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Returns a <see cref="Boolean"/> value which indicates whether the current
        /// <c>ActionDelegatedEventHandler{T}</c> equals to the given object.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> which is used to compare to
        /// the current <c>ActionDelegatedEventHandler{T}</c> instance.</param>
        /// <returns>If the given object equals to the current <c>ActionDelegatedEventHandler{T}</c>
        /// instance, returns true, otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (obj == null)
                return false;
            ActionDelegatedEventHandler<TEvent> other = obj as ActionDelegatedEventHandler<TEvent>;
            if (other == null)
                return false;
            return Equals(this.action, other.action);
        }

        public bool Equals(ActionDelegatedEventHandler<TEvent> other)
        {
            return other != null &&
                   EqualityComparer<Action<TEvent>>.Default.Equals(action, other.action);
        }

        public override int GetHashCode()
        {
            return -1387187753 + EqualityComparer<Action<TEvent>>.Default.GetHashCode(action);
        }

        #endregion

        #region IHandler<TDomainEvent> Members
        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message to be handled.</param>
        public void Handle(TEvent message)
        {
            action(message);
        }

        public static bool operator ==(ActionDelegatedEventHandler<TEvent> handler1, ActionDelegatedEventHandler<TEvent> handler2)
        {
            return EqualityComparer<ActionDelegatedEventHandler<TEvent>>.Default.Equals(handler1, handler2);
        }

        public static bool operator !=(ActionDelegatedEventHandler<TEvent> handler1, ActionDelegatedEventHandler<TEvent> handler2)
        {
            return !(handler1 == handler2);
        }

        #endregion
    }
}